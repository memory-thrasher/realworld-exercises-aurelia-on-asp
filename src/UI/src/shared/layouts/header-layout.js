import {inject } from 'aurelia-dependency-injection';
import {bindable, bindingMode } from 'aurelia-framework';
import {Router } from 'aurelia-router';
import {UserService } from '../../shared/services/user-service';
import {SharedState } from '../state/shared-state';

@inject(UserService, SharedState, Router)
export class HeaderLayout {
  activeRoute = '';
  @bindable({defaultBindingMode: bindingMode.twoWay}) routerConfig;

  constructor(userService, sharedState, router) {
    this.userService = userService;
    this.sharedState = sharedState;
    this.router = router;
  }

  routerConfigChanged(newValue, oldValue) {
    this.activeRoute = newValue.name;
  }

  logout() {
    this.userService.purgeAuth();
    this.router.navigateToRoute('home');
  }
}

