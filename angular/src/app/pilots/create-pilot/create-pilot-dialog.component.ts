import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
    PilotServiceProxy,
    CreatePilotDto,
    AirportDto,
    AirportServiceProxy,
    PlaneDto,
    PlaneServiceProxy,
    PagedResultDtoOfPlaneDto
} from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';

@Component({
    templateUrl: './create-pilot-dialog.component.html',
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
export class CreatePilotDialogComponent extends PagedListingComponentBase<PlaneDto>
    implements OnInit {
    saving = false;
    pilot: CreatePilotDto = new CreatePilotDto();
    airports: AirportDto[] = [];
    planes: PlaneDto[] = [];

    constructor(
        injector: Injector,
        public _pilotService: PilotServiceProxy,
        public _airportService: AirportServiceProxy,
        public _planeService: PlaneServiceProxy,
        private _dialogRef: MatDialogRef<CreatePilotDialogComponent>
    ) {
        super(injector);
    }

    ngOnInit(): void {
        super.ngOnInit();
        this.pilot.planeIds = [];
        this._airportService.getAllAirports().subscribe(result => {
            this.airports = result.items;
        });
    }

    save(): void {
        this.saving = true;

        this._pilotService
            .create(this.pilot)
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

    isPlaneChecked(planeId: number): boolean {
        return _.includes(this.pilot.planeIds, planeId);
    }

    onPlaneChange(pilot: PlaneDto, $event: MatCheckboxChange) {
        if (!$event.checked) {
            var index: number = this.pilot.planeIds.indexOf(pilot.id, 0);
            if (index > -1) {
                this.pilot.planeIds.splice(index, 1);
            }
        } else {
            this.pilot.planeIds.push(pilot.id);
        }
    }

    protected list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function): void {
        this._planeService
            .getAll(null, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfPlaneDto) => {
                this.planes = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(entity: PlaneDto): void {
        throw new Error("Method not implemented.");
    }
}
