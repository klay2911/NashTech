import React, { Suspense } from "react";
import { useRoutes } from "react-router-dom";
import { RequiredAuth } from "../components";
import { CreateBook, CreateCategory, NotFound, UpdateBook } from "../pages";
import DetailsRequest from "../pages/Librarian/BorrowingRequest/DetailsRequest";
import { path } from "./routeContants";

const LoginLazy = React.lazy(() => import("../pages/Login"));
const HomeLazy = React.lazy(() => import("../pages/Home"));
const BooksLazy = React.lazy(() => import("../pages/Books/Books"));
const CategoriesLazy = React.lazy(
  () => import("../pages/Categories/Categories"),
);
const UpdateCategoryLazy = React.lazy(
  () => import("../pages/Categories/UpdateCategory"),
);
const ProfileLazy = React.lazy(() => import("../pages/Profile"));
const DetailsRequestLazy = React.lazy(() => import("../pages/Profile"));

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
          <DetailsRequest />
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
          <CreateBook />
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.bookEdit,
      element: (
        <RequiredAuth>
          <UpdateBook />
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.categories,
      element: (
        <RequiredAuth>
          <Suspense>
            {" "}
            <CategoriesLazy />{" "}
          </Suspense>
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.categoryCreate,
      element: (
        <RequiredAuth>
          <CreateCategory />{" "}
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
      path: path.profile,
      element: (
        <RequiredAuth>
          <Suspense>
            <ProfileLazy />
          </Suspense>{" "}
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
