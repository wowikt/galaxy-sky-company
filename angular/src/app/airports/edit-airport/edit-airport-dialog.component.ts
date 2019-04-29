import { Component, Injector, Optional, Inject, OnInit } from '@angular/core';
import {
    MAT_DIALOG_DATA,
    MatDialogRef,
    MatCheckboxChange
} from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
    AirportServiceProxy,
    AirportDto,
    RoleDto
} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './edit-airport-dialog.component.html',
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
export class EditAirportDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    airport: AirportDto = new AirportDto();
    roles: RoleDto[] = [];
    checkedRolesMap: { [key: string]: boolean } = {};

    constructor(
        injector: Injector,
        public _airportService: AirportServiceProxy,
        private _dialogRef: MatDialogRef<EditAirportDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._airportService.get(this._id).subscribe(result => {
            this.airport = result;
        });
    }

    save(): void {
        this.saving = true;

        this._airportService
            .update(this.airport)
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
