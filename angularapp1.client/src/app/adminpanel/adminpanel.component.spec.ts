import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminpanelComponent } from './adminpanel.component';

describe('AdminpanelComponent', () => {
  let component: AdminpanelComponent;
  let fixture: ComponentFixture<AdminpanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminpanelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminpanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
