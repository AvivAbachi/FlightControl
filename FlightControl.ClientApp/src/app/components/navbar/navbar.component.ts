import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  authenticated = false;

  constructor(private authService: AuthService) {
    authService.tokenChanged.subscribe(() => {
      this.authenticated = !!authService.token;
    });
  }

  logout() {
    this.authService.logout();
  }
}
