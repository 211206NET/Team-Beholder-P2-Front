import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VIP1Component } from './vip1.component';

describe('VIP1Component', () => {
  let component: VIP1Component;
  let fixture: ComponentFixture<VIP1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VIP1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VIP1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
