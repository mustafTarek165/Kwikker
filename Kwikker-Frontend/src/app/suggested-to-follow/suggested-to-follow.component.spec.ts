import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestedToFollowComponent } from './suggested-to-follow.component';

describe('SuggestedToFollowComponent', () => {
  let component: SuggestedToFollowComponent;
  let fixture: ComponentFixture<SuggestedToFollowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SuggestedToFollowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuggestedToFollowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
