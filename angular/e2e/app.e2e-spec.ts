import { GalaxySkyCompanyTemplatePage } from './app.po';

describe('GalaxySkyCompany App', function() {
  let page: GalaxySkyCompanyTemplatePage;

  beforeEach(() => {
    page = new GalaxySkyCompanyTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
