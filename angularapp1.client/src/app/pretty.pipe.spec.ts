import { PrettyPipe } from './pretty.pipe';

describe('PrettyPipe', () => {
  it('create an instance', () => {
    const pipe = new PrettyPipe();
    expect(pipe).toBeTruthy();
  });
});
