import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConstantsService {

private apiUrl: string = 'https://localhost:44323'

constructor() { }

/**
 * Returns API Url
 */
public getApiUrl(): string{
  return this.apiUrl;
}

}
