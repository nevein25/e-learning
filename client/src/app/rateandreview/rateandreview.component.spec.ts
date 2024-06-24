import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RateandreviewComponent } from './rateandreview.component';

describe('RateandreviewComponent', () => {
  let component: RateandreviewComponent;
  let fixture: ComponentFixture<RateandreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RateandreviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RateandreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
