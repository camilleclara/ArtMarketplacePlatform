import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { artisanGuard } from './artisan.guard';

describe('userGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => artisanGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
