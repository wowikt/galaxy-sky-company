import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import {
    PlaneServiceProxy,
    PilotServiceProxy,
    CreatePlaneDto,
    AirportDto,
    AirportServiceProxy,
    PilotDto,
    PagedResultDtoOfPilotDto
} from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';

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
export class CreatePlaneDialogComponent extends PagedListingComponentBase<PilotDto>
    implements OnInit {

    saving = false;
    plane: CreatePlaneDto = new CreatePlaneDto();
    airports: AirportDto[] = [];
    pilots: PilotDto[] = [];
    planeTypes: string[] = [
        this.l("PassengerPlane"),
        this.l("CargoPlane"),
        this.l("SpecialPlane"),
        this.l("Helicopter"),
        this.l("OtherAircraft")
    ];

    constructor(
        injector: Injector,
        public _planeService: PlaneServiceProxy,
        public _airportService: AirportServiceProxy,
        public _pilotService: PilotServiceProxy,
        private _dialogRef: MatDialogRef<CreatePlaneDialogComponent>
    ) {
        super(injector);
    }

    ngOnInit(): void {
        super.ngOnInit();
        this.plane.pilotIds = [];
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

    isPilotChecked(pilotId: number): boolean {
        return _.includes(this.plane.pilotIds, pilotId);
    }

    onPilotChange(pilot: PilotDto, $event: MatCheckboxChange) {
        if (!$event.checked) {
            var index: number = this.plane.pilotIds.indexOf(pilot.id, 0);
            if (index > -1) {
                this.plane.pilotIds.splice(index, 1);
            }
        } else {
            this.plane.pilotIds.push(pilot.id);
        }
    }

    protected list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function): void {
        this._pilotService
            .getAll(null, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfPilotDto) => {
                this.pilots = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(entity: PilotDto): void {
        throw new Error("Method not implemented.");
    }
}
