import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { AirportServiceProxy, AirportDto, PagedResultDtoOfAirportDto } from '@shared/service-proxies/service-proxies';
import { CreateAirportDialogComponent } from './create-airport/create-airport-dialog.component';
import { EditAirportDialogComponent } from './edit-airport/edit-airport-dialog.component';
//import { Moment } from 'moment';

@Component({
    templateUrl: './airports.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
    ]
})
export class AirportsComponent extends PagedListingComponentBase<AirportDto> {
    airports: AirportDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _airportService: AirportServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    createAirport(): void {
        this.showCreateOrEditAirportDialog();
    }

    editAirport(airport: AirportDto): void {
        this.showCreateOrEditAirportDialog(airport.id);
    }

    protected list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        this._airportService
            .getAll(null, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfAirportDto) => {
                this.airports = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(airport: AirportDto): void {
        abp.message.confirm(
            this.l('UserDeleteWarningMessage', airport.name),
            (result: boolean) => {
                if (result) {
                    this._airportService.delete(airport.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    private showCreateOrEditAirportDialog(id?: number): void {
        let createOrEditAirportDialog;
        if (id === undefined || id <= 0) {
            createOrEditAirportDialog = this._dialog.open(CreateAirportDialogComponent);
        } else {
            createOrEditAirportDialog = this._dialog.open(EditAirportDialogComponent, {
                data: id
            });
        }

        createOrEditAirportDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
