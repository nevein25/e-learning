import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { Role } from '../_models/Roles.enum';
import { ToastrService } from 'ngx-toastr';

export const instructorGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if (accountService.role().includes(Role.Instructor))
    return true;
  else 
  {
    toastr.error('Not Allowed');
    return false;
  }
};
