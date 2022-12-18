export class Helper {

    public static paginate(
        totalItems: number,
        pageNumber: number = 1,
        take: number = 10,
        maxPages: number = 10
    ): any {
        // calculate total pages
        let totalPages = Math.ceil(totalItems / take);

        // ensure current page isn't out of range
        if (pageNumber < 1) {
            pageNumber = 1;
        } else if (pageNumber > totalPages) {
            pageNumber = totalPages;
        }

        let startPage: number, endPage: number;
        if (totalPages <= maxPages) {
            // total pages less than max so show all pages
            startPage = 1;
            endPage = totalPages;
        } else {
            // total pages more than max so calculate start and end pages
            let maxPagesBeforeCurrentPage = Math.floor(maxPages / 2);
            let maxPagesAfterCurrentPage = Math.ceil(maxPages / 2) - 1;
            if (pageNumber <= maxPagesBeforeCurrentPage) {
                // current page near the start
                startPage = 1;
                endPage = maxPages;
            } else if (pageNumber + maxPagesAfterCurrentPage >= totalPages) {
                // current page near the end
                startPage = totalPages - maxPages + 1;
                endPage = totalPages;
            } else {
                // current page somewhere in the middle
                startPage = pageNumber - maxPagesBeforeCurrentPage;
                endPage = pageNumber + maxPagesAfterCurrentPage;
            }
        }

        // calculate start and end item indexes
        let startIndex = (pageNumber - 1) * take;
        let endIndex = Math.min(startIndex + take - 1, totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        let pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);

        // return object with all pager properties required by the view
        return {
            totalItems: totalItems,
            pageNumber: pageNumber,
            take: take,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }

}