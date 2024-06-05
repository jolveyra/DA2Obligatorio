import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructorCompanyListComponent } from './constructor-company-list.component';

describe('ConstructorCompanyListComponent', () => {
  let component: ConstructorCompanyListComponent;
  let fixture: ComponentFixture<ConstructorCompanyListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructorCompanyListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructorCompanyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
