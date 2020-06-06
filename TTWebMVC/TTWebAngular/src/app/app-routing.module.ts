import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginActivateGuard } from './guards/logginActivate.guard';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ScheduleJobDefFormComponent } from './schedule-job-def-form/schedule-job-def-form.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home',
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [LoginActivateGuard],
  },
  {
    path: 'userProfile',
    component: UserProfileComponent,
    canActivate: [LoginActivateGuard],
  },
  {
    path: 'scheduleJobDefForm',
    component: ScheduleJobDefFormComponent,
    canActivate: [LoginActivateGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
