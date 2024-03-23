import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherforecastComponent } from './weatherforecast.component';

describe('WeatherforecastComponent', () => {
  let component: WeatherforecastComponent;
  let fixture: ComponentFixture<WeatherforecastComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeatherforecastComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WeatherforecastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
