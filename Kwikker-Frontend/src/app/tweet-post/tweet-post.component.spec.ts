import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TweetPostComponent } from './tweet-post.component';

describe('TweetPostComponent', () => {
  let component: TweetPostComponent;
  let fixture: ComponentFixture<TweetPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TweetPostComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TweetPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
