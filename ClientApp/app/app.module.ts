import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';

import {AthletesModule} from "./athletes/athletes.module";
import {BodyPartsModule} from "./body-parts/body-parts.module";
import {CompletedScheduledExercisesModule} from "./completed-scheduled-exercises/completed-scheduled-exercises.module";
import {DashboardTilesModule} from "./dashboard-tiles/dashboard-tiles.module";
import {DashboardsModule} from "./dashboards/dashboards.module";
import {DaysModule} from "./days/days.module";
import {ExercisesModule} from "./exercises/exercises.module";
import {HomeModule} from "./home/home.module";
import {ScheduledExercisesModule} from "./scheduled-exercises/scheduled-exercises.module";
import {SharedModule} from "./shared/shared.module";
import {UsersModule} from "./users/users.module";
import {TenantsModule} from "./tenants/tenants.module";
import {TilesModule} from "./tiles/tiles.module";

import {AppLeftNavComponent} from "./app-left-nav.component";
import {AppComponent} from './app.component';

import {RoutingModule} from "./app.routing";

const declarables = [
    AppComponent,
    AppLeftNavComponent
];

const providers = [];

@NgModule({
    imports: [
        RoutingModule,
        BrowserModule,
        HttpModule,
        CommonModule,
        FormsModule,
        RouterModule,

        AthletesModule,
        BodyPartsModule,
        CompletedScheduledExercisesModule,
        DashboardTilesModule,
        DashboardsModule,
        DaysModule,
        ExercisesModule,
        HomeModule,
        ScheduledExercisesModule,
        SharedModule,
        TenantsModule,
        TilesModule,
        UsersModule

    ],
    providers: providers,
    declarations: [declarables],
    exports: [declarables],
    bootstrap: [AppComponent]
})
export class AppModule { }

