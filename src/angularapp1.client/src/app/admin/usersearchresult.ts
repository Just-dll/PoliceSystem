export interface UserSearchResult {
    id: number,
    name: string,
    surname: string,
    email: string
}

export interface CaseFilePreview {
    id: number;
    type: string;
    initiationDate: string;
    suspects: UserSearchResult[];
  }