import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstructorsApplicationsComponent } from './instructors-applications.component';

describe('InstructorsApplicationsComponent', () => {
  let component: InstructorsApplicationsComponent;
  let fixture: ComponentFixture<InstructorsApplicationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstructorsApplicationsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InstructorsApplicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
