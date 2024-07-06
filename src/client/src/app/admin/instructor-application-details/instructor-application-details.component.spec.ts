import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstructorApplicationDetailsComponent } from './instructor-application-details.component';

describe('InstructorApplicationDetailsComponent', () => {
  let component: InstructorApplicationDetailsComponent;
  let fixture: ComponentFixture<InstructorApplicationDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstructorApplicationDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InstructorApplicationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
