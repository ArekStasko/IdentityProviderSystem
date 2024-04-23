import { Route, Routes } from 'react-router-dom';
import RoutingConstants from './routingConstants';
import React from 'react';
import MainPage from "../../pages/mainPage/MainPage";

export const MainRouting = () => (
    <Routes>
        <Route
            path={RoutingConstants.root}
            element={<MainPage />}
        />
    </Routes>
);

export default MainRouting;