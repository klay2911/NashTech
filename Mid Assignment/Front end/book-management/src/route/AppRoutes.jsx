import React, { Suspense } from "react";
import { useRoutes } from "react-router-dom";
import { RequiredAuth } from "../components";
import { NotFound } from "../pages";
import { path } from "./routeContants";

const LoginLazy = React.lazy(() => import("../pages/Login"));
const HomeLazy = React.lazy(() => import("../pages/Home"));
const BooksLazy = React.lazy(() => import("../pages/Librarian/Books/Books"));
const CreateBookLazy = React.lazy(
  () => import("../pages/Librarian/Books/CreateBook"),
);
const UpdateBookLazy = React.lazy(
  () => import("../pages/Librarian/Books/UpdateBook"),
);
const CategoriesLazy = React.lazy(
  () => import("../pages/Librarian/Categories/Categories"),
);
const CreateCategoriesLazy = React.lazy(
  () => import("../pages/Librarian/Categories/CreateCategory"),
);
const UpdateCategoryLazy = React.lazy(
  () => import("../pages/Librarian/Categories/UpdateCategory"),
);
const DetailsRequestLazy = React.lazy(() => import("../pages/Librarian/BorrowingRequest/DetailsRequest"));

const WaitingRequestsLazy = React.lazy(
  () => import("../pages/Librarian/BorrowingRequest/WaitingRequests"),
);

export const AppRoutes = () => {
  const elements = useRoutes([
    {
      path: path.home,
      element: (
        <Suspense>
          <HomeLazy />
        </Suspense>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.requestDetails,
      element: (
        <RequiredAuth>
          <Suspense>
            <DetailsRequestLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.books,
      element: (
        <RequiredAuth>
          <Suspense>
            <BooksLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.bookCreate,
      element: (
        <RequiredAuth>
          <Suspense>
            <CreateBookLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.bookEdit,
      element: (
        <RequiredAuth>
          <Suspense>
            <UpdateBookLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.categories,
      element: (
        <RequiredAuth>
          <Suspense>
            <CategoriesLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.categoryCreate,
      element: (
        <RequiredAuth>
          <Suspense>
            <CreateCategoriesLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.categoryEdit,
      element: (
        <RequiredAuth>
          <Suspense>
            <UpdateCategoryLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.requests,
      element: (
        <RequiredAuth>
          <Suspense>
            <WaitingRequestsLazy />
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.login,
      element: (
        <Suspense>
          <LoginLazy />
        </Suspense>
      ),
      errorElement: <NotFound />,
    },
    {
      path: "*",
      element: <NotFound />,
    },
  ]);
  return elements;
};
