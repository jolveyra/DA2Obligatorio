export class Report {
    constructor(
        public filterName: string,
        public pendingRequests: number,
        public inProgressRequests: number,
        public completedRequests: number,
        public avgCompletionTime: number,
    ) {}
}