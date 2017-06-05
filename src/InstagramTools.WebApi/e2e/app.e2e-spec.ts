import { AngularAppPage } from './app.po';

describe('angular-app App', () => {
  let page: AngularAppPage;

  beforeEach(() => {
    page = new AngularAppPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
