import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../_models/Roles.enum';

export const studentGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if (accountService.role().includes(Role.Student))
    return true;
  else {
    toastr.error('Not Allowed');
    return false;
  }
};
