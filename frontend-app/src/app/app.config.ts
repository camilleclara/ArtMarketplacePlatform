import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './login/auth.interceptor';
import { LIGHTBOX_CONFIG, LightboxConfig } from 'ng-gallery/lightbox';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])),
    // importProvidersFrom(HttpClientModule),
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideAnimations(),
    {
      provide: LIGHTBOX_CONFIG,
      useValue: {
        keyboardShortcuts: false,
        exitAnimationTime: 1000
      } as LightboxConfig
    }
  ]
};


