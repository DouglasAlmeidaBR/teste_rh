import { Routes } from '@angular/router';

import { RegisterComponent } from './components/auth/register/register.component';
import { WelcomeComponent } from './components/company/welcome/welcome.component';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyDetailsComponent } from './components/company/company-details/company-details.component';

export const routes: Routes = [
  { path: '', redirectTo: '/register', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'welcome/:fullName/:userId', component: WelcomeComponent },
  { path: 'welcome/:fullName', component: WelcomeComponent },
  { path: 'welcome', component: WelcomeComponent },
  { path: 'company-details/:userId', component: CompanyDetailsComponent },
  { path: 'company-details', component: CompanyDetailsComponent }, 
  { path: 'company-form', component: CompanyFormComponent },
  { path: '**', redirectTo: '/register' }
];