import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaseFilePreviewComponent } from './case-file-preview.component';

describe('CaseFilePreviewComponent', () => {
  let component: CaseFilePreviewComponent;
  let fixture: ComponentFixture<CaseFilePreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CaseFilePreviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CaseFilePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
