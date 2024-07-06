import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoughtCoursesListComponent } from './bought-courses-list.component';

describe('BoughtCoursesListComponent', () => {
  let component: BoughtCoursesListComponent;
  let fixture: ComponentFixture<BoughtCoursesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoughtCoursesListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BoughtCoursesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
