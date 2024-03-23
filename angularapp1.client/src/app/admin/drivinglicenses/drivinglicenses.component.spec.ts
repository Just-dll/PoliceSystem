import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrivinglicensesComponent } from './drivinglicenses.component';

describe('DrivinglicensesComponent', () => {
  let component: DrivinglicensesComponent;
  let fixture: ComponentFixture<DrivinglicensesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DrivinglicensesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DrivinglicensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
