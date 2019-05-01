import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { PlaneServiceProxy, PlaneDto, PagedResultDtoOfPlaneDto } from '@shared/service-proxies/service-proxies';
import { CreatePlaneDialogComponent } from './create-plane/create-plane-dialog.component';
//import { EditPlaneDialogComponent } from './edit-Plane/edit-Plane-dialog.component';
//import { Moment } from 'moment';

@Component({
    templateUrl: './planes.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
    ]
})
export class PlanesComponent extends PagedListingComponentBase<PlaneDto> {
    planes: PlaneDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _PlaneService: PlaneServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    createPlane(): void {
        this.showCreateOrEditPlaneDialog();
    }

    editPlane(plane: PlaneDto): void {
        this.showCreateOrEditPlaneDialog(plane.id);
    }

    tableRowStyle(plane: PlaneDto): object {
        if (plane.code.indexOf('A') >= 0) {
            return { "background-color": "palegreen" };
        } else {
            return {};
        }
    }

    protected list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        this._PlaneService
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

    protected delete(Plane: PlaneDto): void {
        abp.message.confirm(
            this.l('UserDeleteWarningMessage', Plane.name),
            (result: boolean) => {
                if (result) {
                    this._PlaneService.delete(Plane.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    private showCreateOrEditPlaneDialog(id?: number): void {
        let createOrEditPlaneDialog;
        if (id === undefined || id <= 0) {
            createOrEditPlaneDialog = this._dialog.open(CreatePlaneDialogComponent);
        //} else {
        //    createOrEditPlaneDialog = this._dialog.open(EditPlaneDialogComponent, {
        //        data: id
        //    });
        }

        createOrEditPlaneDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
