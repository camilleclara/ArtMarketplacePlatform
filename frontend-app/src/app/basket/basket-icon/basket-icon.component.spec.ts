import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketIconComponent } from './basket-icon.component';

describe('BasketIconComponent', () => {
  let component: BasketIconComponent;
  let fixture: ComponentFixture<BasketIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BasketIconComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasketIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
