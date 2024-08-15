import useTokenTracking from "../hooks/useTokenTracking";
import usePageTracking from "../hooks/usePageTracking";
import React from "react";

interface TrackingServiceProps {
    children: React.ReactNode;
}


const TrackingService = ({children}: TrackingServiceProps) => {
    useTokenTracking()
    usePageTracking()
    return (
        <>
            {children}
        </>
    )
}

export default TrackingService;