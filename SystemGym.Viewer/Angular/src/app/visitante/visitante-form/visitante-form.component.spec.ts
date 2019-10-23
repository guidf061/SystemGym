import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { VisitanteFormComponent } from './visitante-form.component';





describe('VisitanteFormComponent', () => {
  let component: VisitanteFormComponent;
  let fixture: ComponentFixture<VisitanteFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VisitanteFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VisitanteFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
