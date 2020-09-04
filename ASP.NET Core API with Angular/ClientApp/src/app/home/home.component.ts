import { Component, Inject } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public page: Page;

  constructor(@Inject('BASE_URL') baseUrl: string, sanitizer: DomSanitizer) {
    const url = sanitizer.bypassSecurityTrustResourceUrl(baseUrl + 'page');
    this.page = new Page(url); 
  }
}

class Page {
  src: SafeUrl;

  constructor(src: SafeUrl) {
    this.src = src;
  }
}