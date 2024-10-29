import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolloweesComponent } from './followees.component';

describe('FolloweesComponent', () => {
  let component: FolloweesComponent;
  let fixture: ComponentFixture<FolloweesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FolloweesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FolloweesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
