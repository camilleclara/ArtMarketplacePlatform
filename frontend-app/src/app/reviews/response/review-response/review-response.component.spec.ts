import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewResponseComponent } from './review-response.component';

describe('ReviewResponseComponent', () => {
  let component: ReviewResponseComponent;
  let fixture: ComponentFixture<ReviewResponseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewResponseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReviewResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
