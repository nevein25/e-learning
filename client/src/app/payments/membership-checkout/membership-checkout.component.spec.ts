import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembershipCheckoutComponent } from './membership-checkout.component';

describe('MembershipCheckoutComponent', () => {
  let component: MembershipCheckoutComponent;
  let fixture: ComponentFixture<MembershipCheckoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembershipCheckoutComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MembershipCheckoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
