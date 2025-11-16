
export interface ExpirationBannerInterface {
    open: boolean;
    onClose: () => void;
    onRefresh: () => void;
    onLogout: () => void;
}