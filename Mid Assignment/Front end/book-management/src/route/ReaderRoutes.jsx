import React, { Suspense } from "react";
import { useRoutes } from "react-router-dom";
import { RequiredAuth } from "../components";
import {
  BorrowingBooks,
  NotFound,
  UserRequestedBookStatusList,
} from "../pages";
import { path } from "./routeContants";

const HomeLazy = React.lazy(() => import("../pages/Home"));
const LoginLazy = React.lazy(() => import("../pages/Login"));
const DetailsBookLazy = React.lazy(() => import("../pages/Reader/DetailsBook"));
const BorrowingBooksLazy = React.lazy(
  () => import("../pages/Reader/BorrowingBooks"),
);
const BooksUserListLazy = React.lazy(
  () => import("../pages/Reader/UserRequestedBooksStatusList"),
);

export const ReaderRoutes = () => {
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
      path: path.userBooks,
      element: (
        <RequiredAuth>
          <BorrowingBooks />
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.booksUserList,
      element: (
        <RequiredAuth>
          <UserRequestedBookStatusList />
        </RequiredAuth>
      ),
      errorElement: <NotFound />,
    },
    {
      path: path.bookDetails,
      element: (
        <RequiredAuth>
          <Suspense>
            <DetailsBookLazy />
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
