//
//  AppodealBannerViewDelegateGodot.mm
//
//  Created by Dmitrii Feshchenko on 03/12/2022.
//

#import "AppodealPlugin.h"
#import "AppodealBannerViewDelegateGodot.h"

@implementation AppodealBannerViewDelegateGodot

- (void)bannerViewDidLoadAd:(nonnull APDBannerView *)bannerView isPrecache:(BOOL)precache {
    if (AppodealPlugin::get_singleton()) {
        int height = bannerView.adSize.height;
        AppodealPlugin::get_singleton()->emit_signal("on_banner_loaded", height, precache);
    }
}

- (void)bannerView:(nonnull APDBannerView *)bannerView didFailToLoadAdWithError:(nonnull NSError *)error {
    if (AppodealPlugin::get_singleton()) {
        AppodealPlugin::get_singleton()->emit_signal("on_banner_failed_to_load");
    }
}

- (void)bannerViewDidShow:(nonnull APDBannerView *)bannerView {
    if (AppodealPlugin::get_singleton()) {
        AppodealPlugin::get_singleton()->emit_signal("on_banner_shown");
    }
}

- (void)bannerView:(nonnull APDBannerView *)bannerView didFailToPresentWithError:(nonnull NSError *)error {
    if (AppodealPlugin::get_singleton()) {
        AppodealPlugin::get_singleton()->emit_signal("on_banner_show_failed");
    }
}

- (void)bannerViewDidInteract:(nonnull APDBannerView *)bannerView {
    if (AppodealPlugin::get_singleton()) {
        AppodealPlugin::get_singleton()->emit_signal("on_banner_clicked");
    }
}

- (void)bannerViewExpired:(nonnull APDBannerView *)bannerView {
    if (AppodealPlugin::get_singleton()) {
        AppodealPlugin::get_singleton()->emit_signal("on_banner_expired");
    }
}

@end
