package com.example.altas.ui.profile;

import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModel;

import com.android.volley.RequestQueue;
import com.example.altas.Models.ProductStatus;
import com.example.altas.repositories.ProductStatusRepository;

import java.util.ArrayList;

/**
 * public class ProfileViewModel that extends ViewModel
 */
public class ProfileViewModel extends ViewModel {

    MutableLiveData<ArrayList<ProductStatus>> productStatusListMutableLiveData;
    private ProductStatusRepository statusRepo;
    private ArrayList<ProductStatus> productStatusList;

    /**
     * ProfileViewModel constructor
     */
    public ProfileViewModel() {

        // Initialize variables
        statusRepo = new ProductStatusRepository();
        productStatusListMutableLiveData = new MutableLiveData<>();

    }

    /**
     * Calls repo to get statuses
     *
     * @param userId user who's productStatuses we want to get
     * @param queue  API request queue
     */
    void getAllUserProductStatus(String userId, RequestQueue queue) {
        //Request for products
        statusRepo.getAllProductStatus(userId, queue);

        // Observe API response
        statusRepo.productStatusListMutableLiveData.observeForever(new Observer<ArrayList<ProductStatus>>() {
            @Override
            public void onChanged(ArrayList<ProductStatus> productStatuses) {
                productStatusList = productStatuses;
                productStatusListMutableLiveData.setValue(productStatusList);
            }
        });
    }

    /**
     * Removes productStatus from local list and calls repo to remove it from database
     *
     * @param productStatus productStatus that needs to be removed
     * @param queue         API request queue
     */
    void removeProductStatus(ProductStatus productStatus, RequestQueue queue) {

        // Remove productStatus locally
        productStatusList.remove(productStatus);
        productStatusListMutableLiveData.setValue(productStatusList);

        // Call repo to remove it from database
        statusRepo.removeProductStatus(productStatus.id, queue);
    }


    /**
     * Updates product status locally and calls repo to do it in the database as well
     *
     * @param productStatus productStatus that will be updated
     * @param position      of productStatus that needs to be updated
     * @param queue         API request queue
     */
    void confirmDelivered(ProductStatus productStatus, int position, RequestQueue queue) {

        // Update productStatus locally
        productStatusList.set(position, productStatus);
        productStatusListMutableLiveData.setValue(productStatusList);

        // Call repo to update it from database
        statusRepo.updateProductStatus(productStatus, queue);
    }
}
