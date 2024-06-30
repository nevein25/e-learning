import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadedCoursesListComponent } from './uploaded-courses-list.component';

describe('UploadedCoursesListComponent', () => {
  let component: UploadedCoursesListComponent;
  let fixture: ComponentFixture<UploadedCoursesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UploadedCoursesListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UploadedCoursesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
