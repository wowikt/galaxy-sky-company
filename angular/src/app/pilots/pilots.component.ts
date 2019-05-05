import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { PilotServiceProxy, PilotDto, PagedResultDtoOfPilotDto } from '@shared/service-proxies/service-proxies';
import { CreatePilotDialogComponent } from './create-pilot/create-pilot-dialog.component';
import { EditPilotDialogComponent } from './edit-pilot/edit-pilot-dialog.component';
//import { Moment } from 'moment';

@Component({
    templateUrl: './Pilots.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
    ]
})
export class PilotsComponent extends PagedListingComponentBase<PilotDto> {
    pilots: PilotDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _pilotService: PilotServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    createPilot(): void {
        this.showCreateOrEditPilotDialog();
    }

    editPilot(pilot: PilotDto): void {
        this.showCreateOrEditPilotDialog(pilot.id);
    }

    protected list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

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

    protected delete(pilot: PilotDto): void {
        abp.message.confirm(
            this.l('PilotDeleteWarningMessage', pilot.name),
            (result: boolean) => {
                if (result) {
                    this._pilotService.delete(pilot.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    private showCreateOrEditPilotDialog(id?: number): void {
        let createOrEditPilotDialog;
        if (id === undefined || id <= 0) {
            createOrEditPilotDialog = this._dialog.open(CreatePilotDialogComponent);
        } else {
            createOrEditPilotDialog = this._dialog.open(EditPilotDialogComponent, {
                data: id
            });
        }

        createOrEditPilotDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
