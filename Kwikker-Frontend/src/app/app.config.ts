import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { HttpClientModule, provideHttpClient } from '@angular/common/http'; // Import HttpClientModule
import { TimelineService } from '../Services/Timeline.service';
import { TweetService } from '../Services/Tweet.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),  // HttpClient is provided once
            
    
  ]
};
