import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
    PlaneServiceProxy,
    CreatePlaneDto,
    AirportDto,
    AirportServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './create-plane-dialog.component.html',
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
export class CreatePlaneDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    plane: CreatePlaneDto = new CreatePlaneDto();
    airports: AirportDto[] = [];

    constructor(
        injector: Injector,
        public _planeService: PlaneServiceProxy,
        public _airportService: AirportServiceProxy,
        private _dialogRef: MatDialogRef<CreatePlaneDialogComponent>
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._airportService.getAllAirports().subscribe(result => {
            this.airports = result.items;
        });
    }

    save(): void {
        this.saving = true;

        this._planeService
            .create(this.plane)
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
