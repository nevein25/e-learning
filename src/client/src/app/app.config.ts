import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { errorInterceptor } from './_interceptors/error.interceptor';
import { jwtInterceptor } from './_interceptors/jwt.interceptor';
import { httpLoaderInterceptor } from './_interceptors/http-loader.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
              provideHttpClient(withInterceptors([errorInterceptor, jwtInterceptor,httpLoaderInterceptor])),
              provideAnimations(),
              provideToastr({
                positionClass: 'toast-bottom-right'
              }), provideAnimationsAsync()

  ]

};
