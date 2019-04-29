import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
    AirportServiceProxy,
    CreateAirportDto
} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './create-airport-dialog.component.html',
    styles: [
        `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
    ]
})
export class CreateAirportDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    airport: CreateAirportDto = new CreateAirportDto();

    constructor(
        injector: Injector,
        public _airportService: AirportServiceProxy,
        private _dialogRef: MatDialogRef<CreateAirportDialogComponent>
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    save(): void {
        this.saving = true;

        this._airportService
            .create(this.airport)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close(true);
            });
    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
