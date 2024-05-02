import { Route, Routes } from 'react-router-dom';
import RoutingConstants from './routingConstants';
import React from 'react';
import MainPage from "../../pages/mainPage/MainPage";
import Register from "../../components/register/Register";

export const MainRouting = () => (
    <Routes>
        <Route
            path={RoutingConstants.root}
            element={<MainPage />}
        />
        <Route
            path={RoutingConstants.register}
            element={<Register />}
        />
    </Routes>
);

export default MainRouting;