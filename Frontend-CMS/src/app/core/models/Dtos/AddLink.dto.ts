export class AddLinkDto {
    constructor(
        public content: string,
        public pageId: string,
        public siteId: string,
        public currentPageId: string
    ) { }
}