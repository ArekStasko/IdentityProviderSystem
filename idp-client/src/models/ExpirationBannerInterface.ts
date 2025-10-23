
export interface ExpirationBanner {
    close: boolean;
    onClose: () => void;
    onRefresh: () => void;
    onLogout: () => void;
}