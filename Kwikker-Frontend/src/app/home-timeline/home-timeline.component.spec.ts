import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeTimelineComponent } from './home-timeline.component';

describe('HomeTimelineComponent', () => {
  let component: HomeTimelineComponent;
  let fixture: ComponentFixture<HomeTimelineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeTimelineComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeTimelineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
