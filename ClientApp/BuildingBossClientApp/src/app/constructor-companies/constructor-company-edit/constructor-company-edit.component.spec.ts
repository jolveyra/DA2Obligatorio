import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConstructorCompanyEditComponent } from './constructor-company-edit.component';

describe('ConstructorCompanyEditComponent', () => {
  let component: ConstructorCompanyEditComponent;
  let fixture: ComponentFixture<ConstructorCompanyEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConstructorCompanyEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConstructorCompanyEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
