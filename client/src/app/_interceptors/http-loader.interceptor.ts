import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpEvent, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { LoaderService } from '../_services/loader.service';

export const httpLoaderInterceptor: HttpInterceptorFn = (req, next) => {
  const loaderService = inject(LoaderService);
  loaderService.show();

  return next(req).pipe(
    tap(
      (event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          loaderService.hide();
        }
      },
      (error: HttpErrorResponse) => {
        loaderService.hide();
      }
    ),
    finalize(() => {
      loaderService.hide();
    })
  );
};
