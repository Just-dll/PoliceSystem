import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrivinglicenseComponent } from './drivinglicense.component';

describe('DrivinglicenseComponent', () => {
  let component: DrivinglicenseComponent;
  let fixture: ComponentFixture<DrivinglicenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DrivinglicenseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DrivinglicenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
